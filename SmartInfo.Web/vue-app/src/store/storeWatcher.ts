import {deviceStore} from "@/store/deviceStore";

export function watch() {
    console.log('Watcher started')
    let currentBlockId = 0
    deviceStore().$subscribe((mutation, state) => {
        if (!state.block) {
            return
        }
        if (state.block.id && state.block.id === currentBlockId) {
            state.edited = true
        } else if (state.block.id) {
            currentBlockId = state.block.id
        }
    })
}