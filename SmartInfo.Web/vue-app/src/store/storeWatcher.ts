import {deviceStore} from "@/store/deviceStore";

export function watch() {
    console.info('Watcher started')
    let currentBlockId = 0
    deviceStore().$subscribe((mutation, state) => {
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