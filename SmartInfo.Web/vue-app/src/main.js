import { createApp } from 'vue'
import App from './App.vue'
import store from './store'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'
import { aliases, mdi } from 'vuetify/iconsets/mdi'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

//Axios
import axios from 'axios'
import VueAxios from 'vue-axios'

const vuetify = createVuetify({
    components,
    directives,
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
            mdi,
        }
    },
})



createApp(App)
    .use(vuetify)
    .use(store)
    .use(VueAxios, axios)
    .mount('#app')