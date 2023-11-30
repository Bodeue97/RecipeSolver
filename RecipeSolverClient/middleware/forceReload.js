// middleware/forceReload.js
export default function ({ route, redirect }) {
    if (process.client && route.path === '/') {
      window.location.reload(true);
    }
  }
  