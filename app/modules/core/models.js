import { ROOT_TAG_NAME, ROUTES, ROUTES_ROOT, TEMPLATE_ELEMENT_MATCHER } from '../../helpers/constants.js';
import { renderTemplate } from '../../helpers/handlers.js';
export class Component {
    config;
    constructor(config = {
        selector: undefined,
        template: undefined,
        style: undefined
    }){
        this.config = config;
    }
}

export class ApplicationWizzard {
    init = new Event('app-init');
    constructor() {}
    
    bootstrap () {
        document.dispatchEvent(this.init);
    }
}

export class Router {
    routeChanged = new Event('route-changed');
    constructor(){
        this.setupEvents();
    }

    setupEvents() {
        document.addEventListener('change-route', e => {
            window.history.pushState({}, "", '/app' + e.detail.targetRoute);
            this.handleLocation();
        })

        window.addEventListener('popstate', (e) => {
            this.handleLocation();
        })
    }

    async handleLocation() {
        const currentPath = window.location.pathname;
        const path = currentPath.replace('/app', '');
        const route = ROUTES[path] || ROUTES[404];
        const options = {
            method: 'GET',
            mode: 'cors',
            headers: {
                'Content-Type': 'text/html',
                'Accept': 'any'
            },
            credentials: 'include'
        }
        await fetch(`${ROUTES_ROOT}/${route}`, options)
        .then((res) => res.text())
        .then(res => {
            const root = document.querySelector(ROOT_TAG_NAME);
            root.innerHTML = res;
            document.dispatchEvent(this.routeChanged);
            const renderTargets = res.match(TEMPLATE_ELEMENT_MATCHER);
            if(renderTargets){
                renderTargets.forEach(element => {
                    element = element.replace(/[<>]/g,'');
                    renderTemplate(element);
                })
            }
        })
    }
}