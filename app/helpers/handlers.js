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
        if(!stores) return;
          localStorage.setItem('allStores', JSON.stringify(stores))
          console.info(stores);
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
        } else {
          document.querySelector('[login-failed]').removeAttribute('hidden');
        }
      })
    },
    'select-store': (id) => {
      if(!id) return;
      localStorage.setItem("selected_store", id.toString())
      document.dispatchEvent(new CustomEvent("change-route", {
        detail: {
          targetRoute: '/store'
        }
      }))
    },
    'home': () => {
      document.dispatchEvent(new CustomEvent('change-route', {
        detail: {
          targetRoute: '/stores'
        }
      }))
    },
    'employee-list': async (id) => {
      if(id === null || id === undefined) return;
      document.dispatchEvent(new CustomEvent('change-route', {
        detail: {
          targetRoute: '/employee-list'
        }
      }))
      fetch(`${apiUrl}/user/all`, {
        method: "GET",
        mode: "cors",
        headers: BASE_HEADERS
      })
      .then(res => {
        if(res.ok){
          return res.json();
        } else{
          pageHandlers['home']();
        }
      })
      .then(data => {
        if(!data) return;
          console.info(data);
          localStorage.setItem("employeeList", JSON.stringify(data))
          document.dispatchEvent(new CustomEvent('remove-loader'));
          setTimeout(() => {
            for(const employee of data){
              document.dispatchEvent(new CustomEvent('insert-template', {
                detail: {
                  root: 'employee-list-container',
                  template: 'app-employee',
                  data: {
                    id: employee.id,
                    name: employee.name,
                    salary: employee.salary
                  }
                }
              }))
            }
          }, 200);
      })
    },
    'update-employee': async (id) => {
      if(id === null || id === undefined) return;
      const employees = JSON.parse(localStorage.getItem('employeeList'));
      if(!employees) return;
      const employee = employees.find(e => e.id === id);
      document.dispatchEvent(new CustomEvent('change-route', {
        detail: {
          targetRoute: '/update-employee'
        }
      }))

      setTimeout(() => {
        document.querySelector('[update-employee-id]').innerHTML = employee.id;
        document.querySelector('[update-employee-name]').value = employee.name;
        document.querySelector('[update-employee-salary]').value = employee.salary;
      }, 200)
    },
    'update-employee-trigger': async () => {
      const data = {
        id: document.querySelector('[update-employee-id]').innerHTML,
        name: document.querySelector('[update-employee-name]').value,
        salary: document.querySelector('[update-employee-salary]').value
      }
      await fetch(`${apiUrl}/user/${data.id}`, {
        method: 'PUT',
        mode: "cors",
        headers: BASE_HEADERS,
        body: JSON.stringify(data)
      })
      .then(() => pageHandlers['home']())
    },
    'store': () => {
      const storeID = +(localStorage.getItem("selected_store") ?? -1);
      if(storeID !== null){
        fetch(`${apiUrl}/store/${storeID}`, {
          method: 'GET',
          mode: 'cors',
          headers: BASE_HEADERS
        })
        .then(r => {
          if(r.ok){
            return r.json();
          } else {
            document.dispatchEvent(new CustomEvent('change-route', {
              detail: {
                targetRoute: '/stores'
              }
            }))
          }
        })
        .then(store => {
          if(!store) return;
          console.info(store);
          localStorage.setItem("selectedStore", JSON.stringify(store))
          document.dispatchEvent(new CustomEvent('remove-loader'));
          document.dispatchEvent(new CustomEvent('insert-template', {
            detail: {
              root: 'selected-store',
              template: 'app-selected-store',
              data: {
                id: store.id,
                name: store.name
              }
            }
          }))
          setTimeout(() => {
            if(store && store.staff && store.staff.length === 0) {
              document.querySelector('[no-staff]').removeAttribute("hidden");
              return;
            }
            for(const employment of store.staff){
              document.dispatchEvent(new CustomEvent('insert-template', {
                detail: {
                  root: 'selected-store-staff',
                  template: 'app-store-staff',
                  data: {
                    id: employment.id,
                    date: employment.startDate,
                    type: employment.type,
                  }
                }
              }))
            }
          }, 200)
        })
      }
    },
    'select-employment': async (id) => {
      console.log(`Selected employment id: ${id}`);
    },
    'add-employment': (id) => {
      if(id === null || id === undefined) return;
      localStorage.setItem('employment_id', id.toString())
      document.dispatchEvent(new CustomEvent('change-route', {
        detail: {
          targetRoute: '/add-employment'
        }
      }))
    },
    'delete-store': async (id) => {
      await fetch(`${apiUrl}/store/${id}`, {
        method: "DELETE",
        mode: "cors",
        headers: BASE_HEADERS
      })
      .then(res => {
        if(res.ok){
          // alert(`Store with ID: ${id} sucessfully deleted. \n Reload Application to see changes.`);
          // dispatchEvent(new CustomEvent('change-route'), {detail: {targetRoute: '/stores'}});
          window.location.href = '/app/';
        }
      })
    },
    'add-store': () => {
      document.dispatchEvent(new CustomEvent('change-route', {
        detail: {
          targetRoute: '/add-store'
        }
      }))
    },
    'add-store-submit': async () => {
      const data = {
        name: document.querySelector("[new-store]").value
      }
      await fetch(`${apiUrl}/store`, {
        method: 'POST',
        mode: "cors",
        headers: BASE_HEADERS,
        body: JSON.stringify(data)
      })
      .then(res => {
        if(res.ok){
          document.dispatchEvent(new CustomEvent('change-route', { detail: {targetRoute: '/stores'}}))
        }
      })

    },
    'add-employment-submit': async () => {
      const data = {
        userID: +document.querySelector("[employment-user-id]").value,
        storeID: +document.querySelector("[employment-store-id]").value,
        type: document.querySelector("[employment-type]").value
      }
      await fetch(`${apiUrl}/store/new/employment`, {
        method: 'POST',
        mode: "cors",
        headers: BASE_HEADERS,
        body: JSON.stringify(data)
      })
      .then(res => {
        if(res.ok){
          window.location.href = '/app';
        }
      })
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