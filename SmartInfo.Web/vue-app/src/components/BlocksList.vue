<template>
  <v-item-group selected-class="bg-primary">
    <v-container>
      <v-row
          v-for="b in blocks"
          :key="b"
      >
        <v-item v-slot="{ selectedClass, toggle }">
          <v-card
              :class="['d-flex align-center mb-1 w-100', selectedClass]"
              dark
              @click="toggle"
          >
            <v-icon
                :icon="b.icon"
            ></v-icon>
            <div
                class="text-body-1 flex-grow-1 text-left"
            >
              {{ b.caption }}
            </div>
          </v-card>
        </v-item>
      </v-row>
    </v-container>
  </v-item-group>
</template>

<script>
import {blockType, blockIcon} from '@/constants'
import {deviceStore} from '@/store/deviceStore'

export default {
  computed: {
    blocks() {
      return deviceStore().device.blocks.map(b => {
        switch (b.type) {
          case blockType.text:
            b.icon = blockIcon.text
            break
          case blockType.scenario:
            b.icon = blockIcon.scenario
            break
          case blockType.dateTime:
            b.icon = blockIcon.dateTime
            break
          case blockType.picture:
            b.icon = blockIcon.picture
            break
          case blockType.table:
            b.icon = blockIcon.table
            break
        }
        console.log(b)
        return b
      })
    }
  }
}
</script>

<style scoped>

</style>