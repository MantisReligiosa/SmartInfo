import {defineStore} from 'pinia'
import {blockType} from '@/constants'
// import axios from "axios";

export const deviceStore = defineStore('device  ', {
    state: () => {
        return {
            device: {blocks: []},
            block: {},
            resolution: {height: 2160, width: 3840}
        }
    },
    actions: {
        async loadById(deviceId) {
            this.device = {
                id: deviceId,
                blocks: [
                    {
                        type: blockType.text,
                        caption: 'Текст1',
                        id: 111
                    },
                    {
                        type: blockType.dateTime,
                        caption: 'Дата1',
                        id: 222
                    },
                    {
                        type: blockType.picture,
                        caption: 'Изображение1',
                        id: 333
                    },
                    {
                        type: blockType.table,
                        caption: 'Таблица1',
                        id: 444
                    },
                    {
                        type: blockType.scenario,
                        caption: 'Сценарий1',
                        id: 555
                    }
                ]
            }
        },
        selectBlockById(id) {
            this.block = this.device.blocks.filter(d => d.id === id)[0]
        }
    },
})