<template>
  <v-item-group
      selected-class="bg-grey-lighten-2"
      mandatory>
    <v-container>
      <v-row
          v-for="b in blocks"
          :key="b"
      >
        <v-item
            v-slot="{ selectedClass, toggle }"
        >
          <v-container
              :class="['d-flex w-100 pt-1 pb-2', selectedClass]"
              @click="selectBlock(b, toggle)"
          >
            <v-icon
                class="mr-2"
                :icon="b.icon"
            ></v-icon>
            <v-label
                class="text-body-2 flex-grow-1 text-left"
            >
              {{ b.caption }}
            </v-label>
          </v-container>
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
        return b
      })
    }
  },
  methods: {
    selectBlock(o, callback) {
      deviceStore().selectBlockById(o.id)
      callback()
    }
  }
}
</script>

<style scoped>

</style>