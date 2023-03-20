import {defineStore} from 'pinia'


export const mainStore = defineStore('main  ', {
    state: () => {
        return {
            devices: [],
            device: null
        }
    },
    actions: {
        load() {
            this.devices = [
                {
                    'name': 'Device 1',
                    'id': 0
                },
                {
                    'name': 'Device 2',
                    'id': 1
                }
            ]
            this.device = this.devices[0]
        },
        selectDeviceById(deviceId) {
            const device = this.devices.filter(d => d.id == deviceId)[0]
            this.device = device
        }
    },
})