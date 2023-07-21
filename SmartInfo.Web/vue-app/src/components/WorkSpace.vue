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
      >
        <component
            v-for="(block,index) in blocks"
            v-bind:key="index"
            :is="BuildPresentationBlock(block)"
            v-bind:block="blocks[index]"
            :style="{
              'left': block.x * scale + 'px',
              'top': block.y * scale + 'px',
              'height': block.h * scale + 'px',
              'width': block.w * scale + 'px',
              'position': 'absolute',
              'border': '1px black solid'
            }"
        ></component>
      </v-card>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import {computed} from "vue";
import {IDevice} from "@/interfaces/IDevice";
import {BuildPresentationBlock} from "@/components/Presentation/PresentationBlockFactory";

const mStore = mainStore()
const dStore = deviceStore()
const scale = computed(() => mStore.scale)

const h = computed(() => dStore.resolution.height * mStore.scale)
const w = computed(() => dStore.resolution.width * mStore.scale)

const padding = computed(() => 20 * mStore.scale)
const device = computed(() => dStore.device as IDevice)

const blocks = computed(() => {
  if (!device.value?.blocks) {
    return []
  }

  return device.value.blocks
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