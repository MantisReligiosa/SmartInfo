import {createApp} from 'vue'
import App from './App.vue'
import {createPinia} from 'pinia'

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
import {deviceStore} from "@/store/deviceStore";

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

let currentBlockId = null
deviceStore().$subscribe((mutation, state) => {
    if (state.block.id && state.block.id === currentBlockId) {
        state.edited = true
    } else if (state.block.id) {
        currentBlockId = state.block.id
    }
})