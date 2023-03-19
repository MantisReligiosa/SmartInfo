import {createStore} from 'vuex'

export default createStore({
    state: {
        devices: []
    },
    mutations: {
        load(state) {
            state.devices = [
                {
                    'name': 'Устройство 1',
                    'id': 0
                },
                {
                    'name': 'Device 2',
                    'id': 1
                }
            ]
        }
    },
    actions: {
        load({commit}) {
            commit('load')
        }
    },
    getters: {
        devicesList(state) {
            return state.devices.map(v => ({title: v.name, value: v.id}));
        }
    }
})