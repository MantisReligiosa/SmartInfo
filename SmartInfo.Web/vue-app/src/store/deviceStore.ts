import {defineStore} from 'pinia'
import * as Constants from '@/constants'
import * as Blocks from "@/interfaces/Blocks";
import {IDevice} from "@/interfaces/IDevice";
// import axios from "axios";

interface IState {
    device: IDevice | null,
    block: Blocks.IBlock | null,
    edited: boolean,
    resolution: { height: 2160, width: 3840 }
}

export const deviceStore = defineStore('device  ', {
    state: (): IState => {
        return {
            device: {id: 0, blocks: []},
            block: null,
            edited: false,
            resolution: {height: 2160, width: 3840}
        }
    },
    actions: {
        async loadById(deviceId: number | undefined) {
            if (deviceId == undefined) {
                console.log('Failed to load device')
                return
            }
            const textBlock: Blocks.ITextBlock = {
                type: Constants.blockType.text,
                caption: 'Текст1' + deviceId,
                id: 100,
                x: 10,
                y: 20,
                h: 30,
                w: 40,
                text: 'ABCDEF',
                fontId: 1,
                fontSize: 10,
                fontColor: "#fc6b03",
                backColor: "#ffffff",
                formatting: [],
                vAlign: Constants.vAlign.center,
                hAlign: Constants.hAlign.left,
                isAnimationEnabled: true,
                animationStyle: 0,
                holdBeforeAnimation: 1,
                holdAfterAnimation: 2,
                animationSpeed: 5
            }
            const datetimeBlock: Blocks.IDatetimeBlock = {
                type: Constants.blockType.dateTime,
                caption: 'Дата1' + deviceId,
                id: 101,
                x: 11,
                y: 21,
                h: 31,
                w: 41,
                fontId: 1,
                fontSize: 10,
                fontColor: "#fc6b03",
                backColor: "#ffffff",
                formatting: [],
                vAlign: Constants.vAlign.center,
                hAlign: Constants.hAlign.left,
            }
            const tableBlock: Blocks.ITableBlock = {
                type: Constants.blockType.table,
                caption: 'Таблица1' + deviceId,
                id: 103,
                x: 13,
                y: 23,
                h: 33,
                w: 43,
                fontId: 1,
                fontSize: 10,
                fontColor: "#fc6b03",
                backColor: "#ffffff",
                formatting: [],
                vAlign: Constants.vAlign.center,
                hAlign: Constants.hAlign.left,
            }
            const pictureBlock: Blocks.IPictureBlock = {
                type: Constants.blockType.picture,
                caption: 'Изображение1' + deviceId,
                id: 102,
                x: 12,
                y: 22,
                h: 32,
                w: 42
            }
            const scenario: Blocks.IScenario = {
                type: Constants.blockType.scenario,
                caption: 'Сценарий1' + deviceId,
                id: 104,
                x: 14,
                y: 24,
                h: 34,
                w: 44
            }
            this.edited = false
            this.block = null
            this.device = {
                id: deviceId,
                blocks: [
                    textBlock,
                    datetimeBlock,
                    pictureBlock,
                    tableBlock,
                    scenario
                ]
            }
        },
        selectBlockById(id: number) {
            if (this.device && this.device.blocks) {
                this.block = this.device.blocks.filter(d => d.id === id)[0]
            }
        }
    },
})