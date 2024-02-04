import colors from 'vuetify/es5/util/colors'
import https from 'https'; // Import HTTPS module


export default {
  // Global page headers: https://go.nuxtjs.dev/config-head
  head: {
    titleTemplate: '%s - RecipeSolverClient',
    title: 'RecipeSolverClient',
    htmlAttrs: {
      lang: 'en',
    },
    meta: [
      { charset: 'utf-8' },
      { name: 'viewport', content: 'width=device-width, initial-scale=1' },
      { hid: 'description', name: 'description', content: '' },
      { name: 'format-detection', content: 'telephone=no' },
    ],
    link: [
      { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },
      // Preconnect to fonts.gstatic.com
      {
        rel: 'preconnect',
        href: 'https://fonts.gstatic.com',
        crossorigin: true,
      },
      // Link to Google Fonts with font-display swap
      {
        rel: 'stylesheet',
        href: 'https://fonts.googleapis.com/css2?family=Saira+Semi+Condensed:wght@300&family=Teko&display=swap',
      },
    ],
  },
  

  // Global CSS: https://go.nuxtjs.dev/config-css
  css: [
    '@/assets/global.scss',
  ],

  // Plugins to run before rendering page: https://go.nuxtjs.dev/config-plugins
  plugins: [
    '~/plugins/validation.js',
    '@/plugins/vuetify',
    '@/plugins/axios',

  ],
  render: {
    http2: {
      push: true,
    },
    static: {
      maxAge:  0, // Adjust cache duration as needed
    },
  },

  // Auto import components: https://go.nuxtjs.dev/config-components
  components: true,

  // Modules for dev and build (recommended): https://go.nuxtjs.dev/config-modules
  buildModules: [
    // https://go.nuxtjs.dev/vuetify
    '@nuxtjs/vuetify',
  ],

  // Modules: https://go.nuxtjs.dev/config-modules
  modules: [
    '@nuxtjs/axios',
    '@nuxtjs/auth-next',
  ],
  auth: {
    strategies: {
      local: {
        user: {
          property: 'user'
        },
        endpoints: {
          login: { url: '/api/auth/login', method: 'post' },
          logout: { },
          user: false // Disable automatic fetching of user data after login
        }
      }
    },
    redirect: {
      login: '/login',
      logout: '/',
      home: '/'
    }
  },
  

  store: true,
  

  // Axios module configuration: https://go.nuxtjs.dev/config-axios
  axios: {
    baseURL: process.env.API_BASE_URL || 'http://localhost:5041',
    httpsAgent: process.env.NODE_ENV === 'development' ? new https.Agent({ rejectUnauthorized: false }) : undefined,
  },
  
  
  router: {
    middleware: ['auth'],
  },

  // Vuetify module configuration: https://go.nuxtjs.dev/config-vuetify
  vuetify: {
    customVariables: ['~/assets/variables.scss'],
    theme: {
      dark: true,
      themes: {
        dark: {
          primary: colors.blue.darken2,
          accent: colors.grey.darken3,
          secondary: colors.amber.darken3,
          info: colors.teal.lighten1,
          warning: colors.amber.base,
          error: colors.deepOrange.accent4,
          success: colors.green.accent3,
        },
      },
    },
  },

  // Build Configuration: https://go.nuxtjs.dev/config-build
  build: {
   
  },
}
