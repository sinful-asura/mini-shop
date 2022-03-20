import { apiUrl } from "../environment.js";
import { FETCH_OPTIONS, APP, HEADER_TAG_NAME, SIDEBAR_TAG_NAME } from "./constants.js";
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
    'stores': async () => {
      await fetch(`${apiUrl}/store/all`, FETCH_OPTIONS)
      .then(res => res.json())
      .then(stores => {
        console.log(stores);
      })
    },
    'try-login': () => {
      console.log('Try login triggered');
      const root = document.querySelector(APP);
      if(root){
        root.classList.remove('guest');
        renderTemplate(HEADER_TAG_NAME);
        renderTemplate(SIDEBAR_TAG_NAME);
        document.dispatchEvent(
          new CustomEvent("change-route", {
            detail: {
              targetRoute: '/stores',
            },
          })
        );
      }
    }
}

export function executeHandler(handler) {
  if(pageHandlers[handler]){
    pageHandlers[handler]();
  }
}

document.handlers = pageHandlers;
document.handle = executeHandler;