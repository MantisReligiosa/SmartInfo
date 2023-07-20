<template>
    <v-container
            class="ml-0 ma-1 h-100 workSpace"
    >
        <v-card
                color="black"
                :height="h + 2 * padding"
                :width="w + 2 * padding"
                :style="{'padding-left': padding + 'px', 'padding-top': padding + 'px'}"
                class="elevation-5"
        >
            <v-card
                    :height="h"
                    :width="w"
                    :color="device.backColor"
                    rounded="0"
                    class="pa-0 ma-0"
                    @mousedown="onMouseDown"
            >
                <div
                        :style="{'position': 'relative', 'left': cursorX+'px', 'top': cursorY+'px'}"
                        id="cursor"
                        :hidden="!cursorVisible"
                >
                    <div
                            id="cross"
                            v-if="mStore.editorState.mode===Constants.editorMode.drawing"
                            style="margin-left: -12px; margin-top: -13px"
                    >
                        <v-icon
                                icon="mdi-plus"
                        ></v-icon>
                        <v-icon
                                :icon="blockIcon"
                                size="x-small"
                        ></v-icon>
                    </div>
                    <div
                            id="select"
                            v-if="mStore.editorState.mode===Constants.editorMode.selection"
                            style="margin-left: -7px; margin-top: -6px"
                    >
                        <v-icon
                                icon="mdi-cursor-default-outline"
                        ></v-icon>
                    </div>
                </div>
            </v-card>
        </v-card>
    </v-container>
</template>

<script setup lang="ts">
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import {computed} from "vue";
import * as Constants from '@/constants'
import {IDevice} from "@/interfaces/IDevice";

const mStore = mainStore()
const dStore = deviceStore()

const h = computed(() => dStore.resolution.height * mStore.scale)
const w = computed(() => dStore.resolution.width * mStore.scale)

const padding = computed(() => 20 * mStore.scale)
const device = computed(() => dStore.device as IDevice)

const onMouseDown = (ev:MouseEvent) => {
    console.log(ev.offsetX/ mStore.scale, ev.offsetY / mStore.scale)
}

const blockIcon = computed(() => {
    switch (mStore.editorState.blockType) {
        case Constants.blockType.text:
            return Constants.blockIcon.text
        case Constants.blockType.dateTime:
            return Constants.blockIcon.dateTime
        case Constants.blockType.picture:
            return Constants.blockIcon.picture
        case Constants.blockType.table:
            return Constants.blockIcon.table
        case Constants.blockType.scenario:
            return Constants.blockIcon.scenario
        default:
            return ''
    }
})
</script>

<style scoped>
.workSpace {
    background-color: #eeeeee;
    max-width: 100% !important;
    overflow-x: scroll;
    overflow-y: scroll;
}

</style>