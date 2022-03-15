export function renderTemplate(template){
    if(!template) return;
    document.dispatchEvent(new CustomEvent("render-template", {
      detail: {
        template: template
      },
    }))
    console.info(`%c[Render Template] %c-> %c${template}`, "color: red;", "color: white;", "color: #0076e3");
  }

export const pageHandlers = {
    'login': () => {}
}