import {deviceStore} from "@/store/deviceStore";

export function watch() {
    console.info('Watcher started')
    let currentBlockId = 0
    let deviceName = ''
    let backColor = ''
    deviceStore().$subscribe((mutation, state) => {
        if (state.device) {
            if (deviceName != state.device.name) {
                if (deviceName != '') {
                    state.edited = true
                }
                deviceName = state.device.name
            }
            if (backColor != state.device.backColor) {
                if (backColor != '') {
                    state.edited = true
                }
                backColor = state.device.backColor
            }
        }
        if (!state.block) {
            currentBlockId = 0
            return
        }
        if (state.block.id && state.block.id === currentBlockId) {
            state.edited = true
        } else if (state.block.id) {
            currentBlockId = state.block.id
        }
    })
}