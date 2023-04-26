<template>
    <v-item-group
            selected-class="bg-grey-lighten-2"
            >
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

<script setup lang="ts">
import * as Constants from '@/constants'
import {deviceStore} from '@/store/deviceStore'
import {computed} from "vue";

type callbackDelegate = () => void;

class Item {
    id: number;
    caption: string;
    icon: string;

    constructor(id: number, caption: string, icon: string) {
        this.id = id
        this.caption = caption
        this.icon = icon
    }
}

const dStore = deviceStore()


const blocks = computed<Item[]>(() => {
    if (!dStore.device?.blocks)
        return []

    return dStore.device.blocks.map(b => {
        switch (b.type) {
            case Constants.blockType.text:
                return new Item(b.id, b.caption, Constants.blockIcon.text)

             case Constants.blockType.scenario:
                 return new Item(b.id, b.caption, Constants.blockIcon.scenario)
            case Constants.blockType.dateTime:
                return new Item(b.id, b.caption, Constants.blockIcon.dateTime)
            case Constants.blockType.picture:
                return new Item(b.id, b.caption, Constants.blockIcon.picture)
            case Constants.blockType.table:
                return new Item(b.id, b.caption, Constants.blockIcon.table)
        }
    })
})
const selectBlock = (o: Item, callback: callbackDelegate) => {
    if (dStore.block && dStore.block.id == o.id){
        dStore.deselectBlock()
    }
    else {
        dStore.selectBlockById(o.id)
    }
    callback()
}
</script>

<style scoped>

</style>