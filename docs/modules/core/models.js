import { FETCH_OPTIONS, ROOT_TAG_NAME, ROUTES, ROUTES_ROOT, TEMPLATE_ELEMENT_MATCHER } from '../../helpers/constants.js';
import { renderTemplate } from '../../helpers/handlers.js';

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
            window.history.pushState({}, "", '/mini-shop' + e.detail.targetRoute);
            this.handleLocation();
        })

        window.addEventListener('popstate', (e) => {
            this.handleLocation();
        })
    }

    async handleLocation() {
        const currentLocation = window.location.pathname;
        const path = currentLocation.replace('/mini-shop', '')
        const route = ROUTES[path] || ROUTES[404];
        await fetch(`${ROUTES_ROOT}/${route}`, FETCH_OPTIONS)
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