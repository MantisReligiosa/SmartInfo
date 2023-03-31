import {defineStore} from 'pinia'
import {blockType, mode} from '@/constants'
// import axios from "axios";

const scales = [.01, .02, .03, .04, .05, .06, .07, .08, .09, .1, .15, .20, .33, .50, .75, 1, 1.25, 1.5, 2, 2.5, 3, 4]

const currentScaleIndex = function (scale) {
    return scales.indexOf(scale)
}
export const mainStore = defineStore('main  ', {
    state: () => {
        return {
            devices: [],
            device: null,
            scale: 1,
            editorState: {mode: mode.selection, blocktype: 0}
        }
    },
    actions: {
        async load() {
            this.devices = [
                {
                    'name': 'Device 1',
                    'id': 0,
                },
                {
                    'name': 'Device 2',
                    'id': 1
                }
            ]
            this.device = this.devices[0]
        },
        selectDeviceById(deviceId) {
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
            this.editorState.mode = mode.selection
            this.editorState.blocktype = 0
        },
        switchToDrawMode(value) {
            this.editorState.mode = mode.drawing
            if (Object.values(blockType).includes(value)) {
                this.editorState.blocktype = value
            }
        }
    },
})