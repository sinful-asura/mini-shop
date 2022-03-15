import { ROOT_TAG_NAME, ROUTES, ROUTES_ROOT } from './constants.js';

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
        const path = window.location.pathname;
        console.log(path);
        const route = ROUTES['/mini-shop' + path] || ROUTES[404];
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
        })
    }
}