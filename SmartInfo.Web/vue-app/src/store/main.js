import { defineStore } from 'pinia'
export const mainStore = defineStore('counter', {
    state: () => { 
        return {
        devices: []
    }},
    actions: {
        load() {
            this.devices = [
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
    getters: {
        devicesList() {
            return this.devices.map(v => ({title: v.name, value: v.id}));
        }
    }
})