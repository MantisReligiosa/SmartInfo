import {createApp} from 'vue'
import App from './App.vue'

//Store
import {createPinia} from 'pinia'
import {watch} from './store/storeWatcher'

// Vuetify
import 'vuetify/styles'
import {createVuetify} from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'
import {aliases, mdi} from 'vuetify/iconsets/mdi'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

//Axios
import axios from 'axios'
import VueAxios from 'vue-axios'


const pinia = createPinia()


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
    defaults: {
        global: {
            ripple: false
        },
    }
})

createApp(App)
    .use(vuetify)
    .use(pinia)
    .use(VueAxios, axios)
    .mount('#app')

watch()