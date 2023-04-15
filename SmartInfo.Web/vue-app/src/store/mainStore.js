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
            editorState: {mode: mode.selection, blocktype: 0},
            fontSizes: [],
            fontNames: []
        }
    },
    actions: {
        async load() {
            this.fontSizes = [10, 12, 14, 16, 20, 24, 34, 48, 60, 96]
            this.fontNames = [
                {id: 1, name: 'Roboto'}, 
                {id: 2, name: 'Open Sans'}, 
                {id: 3, name: 'Noto Sans JP'},
                {id: 4, name: 'Monsterrat'}, 
                {id: 5, name: 'Lato'}, 
                {id: 6, name: 'Poppins'},
                {id: 7, name: 'Roboto Condensed'}, 
                {id: 8, name: 'Source Sans Pro'},
                {id: 9, name: 'Inter'}, 
                {id: 10, name: 'Roboto Mono'}, 
                {id: 11, name: 'Oswald'}, 
                {id: 12,name: 'Noto Sans'}]
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