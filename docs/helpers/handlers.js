export function renderTemplate(template, replaceWith = null){
    if(!template) return;
    document.dispatchEvent(new CustomEvent("render-template", {
      detail: {
        template: template,
        replaceWith: replaceWith
      },
    }))
    console.info(`%c[Render Template] %c-> %c${template}`, "color: red;", "color: white;", "color: #0076e3");
  }

export const pageHandlers = {
    'login': () => {}
}