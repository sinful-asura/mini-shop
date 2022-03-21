import { INTERPOLATION_MATCHER, LOADER_TAG_NAME, ROOT_TAG_NAME, TEMPLATES_ROOT } from "./helpers/constants.js";

function _findElement(e) {
    return document.querySelector(e.detail.template);
}

document.addEventListener('render-template', async (e) => {
    if(!e || !e.detail || !e.detail.template) return;
    const target = _findElement(e);
    const template = target.nodeName.toLowerCase().replace('app-', '');
    await fetch(`${TEMPLATES_ROOT}/${template}.html`)
    .then(res => res.text())
    .then(template => {
        if(template.includes('<title>Error</title>')) {
            console.info(`Fetch failed for <${e.detail.template}>`)
            return;
        }
        target.innerHTML = template;
    })
})

document.addEventListener('remove-loader', async () => {
    const target = document.querySelector(ROOT_TAG_NAME);
    const spinner = target.querySelector(LOADER_TAG_NAME);
    if(spinner){
        target.removeChild(spinner);
    }
    const hiddenChildren = target.querySelectorAll('.is-loading');
    hiddenChildren.forEach(child => {
        child.classList.remove('is-loading');
    })
})

document.addEventListener('insert-template', async (e) => {
    if(!e || !e.detail || !e.detail.template) return;
    const app = document.querySelector(ROOT_TAG_NAME);
    const root = app.querySelector(e.detail.root);
    if(!root) return;
    const template = e.detail.template.replace('app-', '');
    await fetch(`${TEMPLATES_ROOT}/${template}.html`)
    .then(res => res.text())
    .then(template => {
        if(template.includes('<title>Error</title>')) {
            console.info(`Fetch failed for <${e.detail.template}>`)
            return;
        }
        const a = template.match(INTERPOLATION_MATCHER);
        a.forEach(match => {
            match = match.replace(/[{}]/g, '');
            if(e.detail.data[match]){
                const pattern = `{{${match}}}`;
                const reg = new RegExp(pattern, "g");
                template = template.replace(reg, e.detail.data[match]);
            }
        })
        root.innerHTML += template;
    })
})