import { apiUrl } from "../environment.js";
import { APP, HEADER_TAG_NAME, SIDEBAR_TAG_NAME, BASE_HEADERS } from "./constants.js";
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
      await fetch(`${apiUrl}/store/all`, {
        method: 'GET',
        mode: 'cors',
        headers: BASE_HEADERS
      })
      .then(res => res.json())
      .then(stores => {
          console.log(stores);
          document.dispatchEvent(new CustomEvent('remove-loader'));
          for(const store of stores) {
            document.dispatchEvent(new CustomEvent('insert-template', {
              detail: {
                root: 'stores-container',
                template: 'app-store',
                data: {
                  id: store.id,
                  name: store.name
                }
              }
            }))
          }
      })
    },
    'try-login': async () => {
      const data = {
        username: document.querySelector("input[username]").value,
        password: document.querySelector("input[password]").value
      }
      if(!data.username || !data.password) return;
      await fetch(`${apiUrl}/auth/login`, {
        method: 'POST',
        mode: 'cors',
        headers: BASE_HEADERS,
        body: JSON.stringify(data)
      })
      .then(res => {
        if(res.ok){
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
      })
    },
    'select-store': (id) => {
      if(!id) return;
      localStorage.setItem("selected_store", id.toString())
    }
}

export function executeHandler(handler, params) {
  if(pageHandlers[handler]){
    if(params) {
      pageHandlers[handler](params);
    } else {
      pageHandlers[handler]();
    }
  }
}

document.handlers = pageHandlers;
document.handle = executeHandler;