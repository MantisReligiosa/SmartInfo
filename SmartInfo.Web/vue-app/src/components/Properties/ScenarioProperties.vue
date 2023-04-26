<template>
    <v-container class="px-1">
        <v-list>
            <v-list-item
                    v-for="(item, index) in scenes"
                    :key="item"
                    class="pa-1"
            >
                <v-row
                        no-gutters
                >
                    <v-col class="v-col-2">
                        <v-switch
                                density="compact"
                                v-model="item.isEnabled"
                                class="mt-3"
                        ></v-switch>
                    </v-col>
                    <v-col>
                        <v-row
                                no-gutters
                        >
                            <v-text-field
                                    density="compact"
                                    hide-details
                                    variant="underlined"
                                    v-model="item.caption"
                                    class="noGutters pl-2"
                            ></v-text-field>
                        </v-row>
                        <v-row
                                no-gutters
                        >
                            <v-col>
                                <v-text-field
                                        hide-details
                                        single-line
                                        type="number"
                                        v-model="item.demonstrationPeriod"
                                        min="1"
                                        density="compact"
                                        variant="underlined"
                                        prepend-icon="mdi-clock-outline"
                                        class="noGutters pl-2"
                                ></v-text-field>
                            </v-col>
                            <v-col class="v-col-4">
                                <v-btn
                                        size="small"
                                        variant="text"
                                        class="mt-2"
                                        @click="remove(item)"
                                >
                                    <v-icon
                                            color="red"
                                            icon="mdi-delete-outline"
                                    ></v-icon>
                                </v-btn>
                            </v-col>
                        </v-row>
                    </v-col>
                    <v-col class="v-col-3">
                        <v-row
                                class="mt-3"
                                no-gutters
                        >
                            <v-btn
                                    size="small"
                                    variant="text"
                                    :disabled="index==0"
                                    @click="move(item, -1)"
                            >
                                <v-icon
                                        size="x-large"
                                        icon="mdi-chevron-up-box-outline"
                                ></v-icon>
                            </v-btn>

                        </v-row>
                        <v-row
                                no-gutters
                        >
                            <v-btn
                                    size="small"
                                    variant="text"
                                    :disabled="index==scenes.length-1"
                                    @click=move(item,1)
                            >
                                <v-icon
                                        size="x-large"
                                        icon="mdi-chevron-down-box-outline"
                                ></v-icon>
                            </v-btn>

                        </v-row>
                    </v-col>
                </v-row>
                <v-divider></v-divider>
            </v-list-item>
        </v-list>
        <v-btn
                block
        >
            <v-icon
                    icon="mdi-plus"
                    color="green"
                    @click="add"
            ></v-icon>
        </v-btn>
    </v-container>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {IScenario, IScene} from "@/interfaces/Blocks";

const dStore = deviceStore()

const block = computed(() => dStore.block as IScenario)
const scenes = computed(() => [...block.value.scenes].sort((a, b) => {
    return a.order - b.order
}))

const move = (scene: IScene, direction: number) => {
    let increment = direction > 0 ? 1 : -1;
    let sceneToSwitch = block.value.scenes.find(s => s.order == scene.order + increment)
    if (!sceneToSwitch) {
        return
    }
    let currentOrder = scene.order
    scene.order = sceneToSwitch.order
    sceneToSwitch.order = currentOrder
}

const remove = (scene: IScene) => {
    let arr = block.value.scenes
    const order = scene.order
    const index = arr.indexOf(scene)
    console.log({scene: scene})
    if (index > -1) {
        arr.splice(index, 1)
    }
    arr.forEach(s => {
        if (s.order > order) {
            s.order = s.order - 1
        }
    })
}

const add = () => {
    block.value.scenes.push({
        id: (block.value.scenes.length + 1) * 11,
        caption: 'Новая сцена',
        isEnabled: true,
        demonstrationPeriod: 10,
        order: block.value.scenes.length + 1
    })
}

</script>

<style scoped>
.noGutters :deep(input) {
    padding-top: 0 !important;
}
</style>