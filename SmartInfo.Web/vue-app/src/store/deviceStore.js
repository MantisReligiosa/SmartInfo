import {defineStore} from 'pinia'
import {blockType} from '@/constants'
// import axios from "axios";

const defaultBlock = null
export const deviceStore = defineStore('device  ', {
    state: () => {
        return {
            device: {blocks: []},
            block: {defaultBlock},
            edited: false,
            resolution: {height: 2160, width: 3840}
        }
    },
    actions: {
        async loadById(deviceId) {
            this.edited=false
            this.block=defaultBlock
            this.device = {
                id: deviceId,
                blocks: [
                    {
                        type: blockType.text,
                        caption: 'Текст1',
                        id: 100,
                        x:10,
                        y:20,
                        h:30,
                        w:40,
                        fontId:1,
                        fontSize:10,
                        fontColor: "#fc6b03"
                    },
                    {
                        type: blockType.dateTime,
                        caption: 'Дата1',
                        id: 101,
                        x:11,
                        y:21,
                        h:31,
                        w:41,
                        fontColor: "#fc6b03"
                    },
                    {
                        type: blockType.picture,
                        caption: 'Изображение1',
                        id: 102,
                        x:12,
                        y:22,
                        h:32,
                        w:42
                    },
                    {
                        type: blockType.table,
                        caption: 'Таблица1',
                        id: 103,
                        x:13,
                        y:23,
                        h:33,
                        w:43,
                        fontColor: "#fc6b03"
                    },
                    {
                        type: blockType.scenario,
                        caption: 'Сценарий1',
                        id: 104,
                        x:14,
                        y:24,
                        h:34,
                        w:44
                    }
                ]
            }
        },
        selectBlockById(id) {
            this.block = this.device.blocks.filter(d => d.id === id)[0]
        }
    },
})