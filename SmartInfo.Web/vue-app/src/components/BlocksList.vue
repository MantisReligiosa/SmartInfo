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

<script setup>
import * as Constants from '@/constants'
import {deviceStore} from '@/store/deviceStore'
import {computed} from "vue";

const dStore = deviceStore()

const blocks = computed(() => dStore.device.blocks.map(b => {
    switch (b.type) {
        case Constants.blockType.text:
            b.icon = Constants.blockIcon.text
            break
        case Constants.blockType.scenario:
            b.icon = Constants.blockIcon.scenario
            break
        case Constants.blockType.dateTime:
            b.icon = Constants.blockIcon.dateTime
            break
        case Constants.blockType.picture:
            b.icon = Constants.blockIcon.picture
            break
        case Constants.blockType.table:
            b.icon = Constants.blockIcon.table
            break
    }
    return b
}))

const selectBlock = (o, callback) => {
    dStore.selectBlockById(o.id)
    callback()
}
</script>

<style scoped>

</style>