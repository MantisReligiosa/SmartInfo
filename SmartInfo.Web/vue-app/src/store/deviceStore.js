import {defineStore} from 'pinia'
import {blockType} from '@/constants'
// import axios from "axios";

export const deviceStore = defineStore('device  ', {
    state: () => {
        return {
            device: {blocks: []},
            block: {type: 0},
            edited: false,
            resolution: {height: 2160, width: 3840}
        }
    },
    actions: {
        async loadById(deviceId) {
            this.edited=false
            this.device = {
                id: deviceId,
                blocks: [
                    {
                        type: blockType.text,
                        caption: 'Текст1',
                        id: 111,
                        x:10,
                        y:20,
                    },
                    {
                        type: blockType.dateTime,
                        caption: 'Дата1',
                        id: 222,
                        x:11,
                        y:21
                    },
                    {
                        type: blockType.picture,
                        caption: 'Изображение1',
                        id: 333,
                        x:12,
                        y:22
                    },
                    {
                        type: blockType.table,
                        caption: 'Таблица1',
                        id: 444,
                        x:13,
                        y:23
                    },
                    {
                        type: blockType.scenario,
                        caption: 'Сценарий1',
                        id: 555,
                        x:14,
                        y:24
                    }
                ]
            }
        },
        selectBlockById(id) {
            this.block = this.device.blocks.filter(d => d.id === id)[0]
        }
    },
})