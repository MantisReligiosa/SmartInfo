import {defineStore} from 'pinia'
import {blockType, editorMode} from '@/constants'
// import axios from "axios";

const scales = [.01, .02, .03, .04, .05, .06, .07, .08, .09, .1, .15, .20, .33, .50, .75, 1, 1.25, 1.5, 2, 2.5, 3, 4]

const currentScaleIndex = function (scale: number) {
    return scales.indexOf(scale)
}

interface IFontItem {
    id: number,
    name: string
}

interface IDeviceItem {
    id: number,
    name: string
}

interface IEditorState {
    mode: editorMode,
    blockType: blockType
}

interface IState {
    devices: IDeviceItem[],
    device: IDeviceItem | null,
    scale: number,
    editorState: IEditorState,
    fontSizes: number[]
    fontNames: IFontItem[]
}

export const mainStore = defineStore('main  ', {
    state: (): IState => {
        return {
            devices: [],
            device: null,
            scale: 1,
            editorState: {mode: editorMode.selection, blockType: blockType.text},
            fontSizes: [],
            fontNames: []
        }
    },
    actions: {
        async load() {
            this.fontSizes = [10, 12, 14, 16, 20, 24, 34, 48, 60, 96]
            this.fontNames = [
                {id: 1, name: 'Roboto'},
                {id: 2, name: 'OpenSans'},
                {id: 4, name: 'Montserrat'},

            ]
            this.devices = [
                {
                    name: "Device 1",
                    id: 0,
                },
                {
                    name: 'Device 2',
                    id: 1
                }
            ]
            this.device = this.devices[0]
        },
        selectDeviceById(deviceId: number) {
            this.device = this.devices.filter(d => d.id === deviceId)[0]
        },
        zoomOut() {
            const currentIndex = currentScaleIndex(this.scale)

            if (currentIndex > 0) {
                this.scale = scales[currentIndex - 1]
            }
        },
        zoomIn() {
            const currentIndex = currentScaleIndex(this.scale)

            if (currentIndex < scales.length - 1) {
                this.scale = scales[currentIndex + 1]
            }
        },
        zoomDefault() {
            this.scale = 1
        },
        switchToSelectionMode() {
            this.editorState.mode = editorMode.selection
            this.editorState.blockType = 0
        },
        switchToDrawMode(value: blockType) {
            this.editorState.mode = editorMode.drawing
            if (Object.values(blockType).includes(value)) {
                this.editorState.blockType = value
            }
        }
    },
})