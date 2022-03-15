export const ROOT_TAG_NAME = 'app-content';
export const HEADER_TAG_NAME = 'app-header';
export const SIDEBAR_TAG_NAME = 'app-sidebar';
export const LOADER_TAG_NAME = 'app-spinner';

export const ROUTES_ROOT = './routes';
export const TEMPLATES_ROOT = './templates';
export const TEMPLATE_ELEMENT_MATCHER = /<app-[A-Za-z]*>/g;

export const ROUTES = {
    404: "not-found.html",
    "/": "root.component.html",
    "/login": "login.component.html",
}

export const FETCH_OPTIONS = {
    method: 'GET',
    mode: 'cors',
    headers: {
        'Content-Type': 'text/html',
        'Accept': 'any'
    },
    credentials: 'include'
}