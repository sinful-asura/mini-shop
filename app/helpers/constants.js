export const APP = 'app-root';
export const ROOT_TAG_NAME = 'app-content';
export const HEADER_TAG_NAME = 'app-header';
export const SIDEBAR_TAG_NAME = 'app-sidebar';
export const LOADER_TAG_NAME = 'app-spinner';

export const ROUTES_ROOT = './routes';
export const TEMPLATES_ROOT = './templates';
export const TEMPLATE_ELEMENT_MATCHER = /<app-[a-z\-]*>/g;
export const INTERPOLATION_MATCHER = /{{[A-Za-z0-9]*}}/g;

export const ROUTES = {
    404: "not-found.html",
    "/": "login.component.html",
    "/stores": "stores.component.html",
    "/store": "store.component.html",
    '/add-store': 'add-store.component.html',
    '/add-employment': 'add-employment.component.html'
}

export const FETCH_OPTIONS = {
    method: 'GET',
    mode: 'cors',
    headers: {
        'Content-Type': 'text/html',
        'Accept': '*/*'
    },
    credentials: 'include'
}

export const BASE_HEADERS = {
    'Content-Type': 'application/json, text/plain',
    'Accept': '*/*'
}