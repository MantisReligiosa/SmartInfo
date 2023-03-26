import {defineStore} from 'pinia'
import {blockType} from '@/constants'
// import axios from "axios";

export const deviceStore = defineStore('device  ', {
    state: () => {
        return {
            device: {blocks:[]},
        }
    },
    actions: {
        async loadById(deviceId) {
            this.device = {
                id: deviceId,
                blocks:[
                    {
                        type: blockType.text,
                        caption: 'Текст1'
                    },
                    {
                        type: blockType.dateTime,
                        caption: 'Дата1'
                    },
                    {
                        type: blockType.picture,
                        caption: 'Изображение1'
                    },
                    {
                        type: blockType.table,
                        caption: 'Таблица1'
                    },
                    {
                        type: blockType.scenario,
                        caption: 'Сценарий1'
                    }
                ]
            }
        },
    },
})