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
                    <v-card
                            :class="['d-flex w-100 pt-1 pb-2 flex-column', selectedClass]"
                            @click="selectBlock(b, toggle)"
                    >
                        <div>
                            <v-icon
                                    class="mr-2"
                                    :icon="b.icon"
                            ></v-icon>
                            <v-label
                                    class="text-body-2 flex-grow-1 text-left"
                            >
                                {{ b.caption }}
                            </v-label>
                        </div>
                        <SceneScroller
                                v-if="b.type==Constants.blockType.scenario"
                                :scenes="getScenesByScenarioId(b.id)"
                                @selection-changed="(scene)=>selectScene(b.id, scene)"
                        ></SceneScroller>
                    </v-card>
                </v-item>
            </v-row>
        </v-container>
    </v-item-group>
</template>

<script setup lang="ts">
import * as Constants from '@/constants'
import SceneScroller from "@/components/SceneScroller.vue";
import {deviceStore} from '@/store/deviceStore'
import {computed} from "vue";
import {IScenario, IScene} from "@/interfaces/Blocks";

type callbackDelegate = () => void;

class Item {
    id: number;
    caption: string;
    icon: string;
    type: Constants.blockType

    constructor(id: number, caption: string, icon: string, type: Constants.blockType) {
        this.id = id
        this.caption = caption
        this.icon = icon
        this.type = type
    }
}

const dStore = deviceStore()

const blocks = computed<Item[]>(() => {
    if (!dStore.device?.blocks)
        return []

    return dStore.device.blocks.map(b => {
        switch (b.type) {
            case Constants.blockType.text:
                return new Item(b.id, b.caption, Constants.blockIcon.text, b.type)
            case Constants.blockType.scenario:
                return new Item(b.id, b.caption, Constants.blockIcon.scenario, b.type)
            case Constants.blockType.dateTime:
                return new Item(b.id, b.caption, Constants.blockIcon.dateTime, b.type)
            case Constants.blockType.picture:
                return new Item(b.id, b.caption, Constants.blockIcon.picture, b.type)
            case Constants.blockType.table:
                return new Item(b.id, b.caption, Constants.blockIcon.table, b.type)
        }
    })
})
const selectBlock = (o: Item, callback: callbackDelegate) => {
    if (dStore.block && dStore.block.id == o.id) {
        dStore.deselectBlock()
    } else {
        dStore.selectBlockById(o.id)
    }
    callback()
}
const getScenesByScenarioId = (id: number) => {
    if (!dStore.device) {
        return [];
    }
    const scenario = dStore.device.blocks.find(b => b.type === Constants.blockType.scenario && b.id === id) as IScenario
    return scenario.scenes
}
const selectScene = (scenarioId: number, scene: IScene) => {
    getScenesByScenarioId(scenarioId).forEach(s => s.selected = false)
    scene.selected = true
}

</script>

<style scoped>

</style>