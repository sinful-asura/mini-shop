import { LOADER_TAG_NAME, ROOT_TAG_NAME, TEMPLATES_ROOT } from "./helpers/constants.js";

setTimeout(() => {
    document.dispatchEvent(new CustomEvent('remove-loader'))
}, 1500)

function _findElement(e) {
    if(!e || !e.detail || !e.detail.template) return;
    return document.querySelector(e.detail.template);
}

document.addEventListener('render-template', async (e) => {
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
    target.removeChild(spinner);
    const hiddenChildren = target.querySelectorAll('.is-loading');
    hiddenChildren.forEach(child => {
        child.classList.remove('is-loading');
    })
})
