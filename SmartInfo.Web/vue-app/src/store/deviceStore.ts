import {defineStore} from 'pinia'
import * as Constants from '@/constants'
import {imageMode} from '@/constants'
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
            device: {
                id: 0, 
                blocks: [], 
                name: '',
                backColor: "#ffffff",
            },
            block: null,
            edited: false,
            resolution: {height: 2160, width: 3840}
        }
    },
    actions: {
        async loadById(deviceId: number | undefined) {
            if (deviceId == undefined) {
                console.warn('Failed to load device')
                return
            }
            const textBlock: Blocks.ITextBlock = {
                type: Constants.blockType.text,
                caption: 'Текст1' + deviceId,
                id: 100,
                x: 205,
                y: 20,
                h: 30,
                w: 55,
                z: 50,
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
            const textBlock1: Blocks.ITextBlock = {
                type: Constants.blockType.text,
                caption: 'Текст2' + deviceId,
                id: 110,
                x: 110,
                y: 120,
                h: 130,
                w: 140,
                z: 150,
                text: 'AAAAAAAAA',
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
                x: 90,
                y: 21,
                h: 31,
                w: 75,
                z: 51,
                fontId: 1,
                formatId: 2,
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
                x: 300,
                y: 120,
                h: 150,
                w: 150,
                z: 53,
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
                x: 300,
                y: 20,
                h: 80,
                w: 70,
                z: 52,
                base64: '',
                imageMode: imageMode.zoom,
                saveProportions: false,
            }
            const scenario: Blocks.IScenario = {
                type: Constants.blockType.scenario,
                caption: 'Сценарий1' + deviceId,
                id: 104,
                x: 500,
                y: 24,
                h: 200,
                w: 200,
                z: 54,
                scenes: [
                    {
                        id: 22,
                        caption: 'Scene 2',
                        isEnabled: true,
                        demonstrationPeriod: 20,
                        order: 2,
                        selected: false,
                    },
                    {
                        id: 11,
                        caption: 'Scene 1',
                        isEnabled: true,
                        demonstrationPeriod: 10,
                        order: 1,
                        selected: true,
                    },
                    {
                        id: 33,
                        caption: 'Scene 3',
                        isEnabled: true,
                        demonstrationPeriod: 30,
                        order: 3,
                        selected: false,
                    },
                    {
                        id: 44,
                        caption: 'Disabled',
                        isEnabled: false,
                        demonstrationPeriod: 40,
                        order: 4,
                        selected: false,
                    },
                ]
            }
            this.edited = false
            this.block = null
            this.device = {
                id: deviceId,
                name: 'Девайс',
                backColor: "#987654",
                blocks: [
                    textBlock,
                    textBlock1,
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
        },
        deselectBlock() {
            this.block = null
        }
    },
})