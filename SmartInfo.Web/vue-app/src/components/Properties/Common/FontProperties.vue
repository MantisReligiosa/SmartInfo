<template>
    <v-container>
        <v-row>
            <v-col>
                <v-select
                        :items="fontNames"
                        item-title="name"
                        item-value="id"
                        :model-value="fontId"
                        @update:modelValue="setFont"
                        density="compact"
                        variant="underlined"
                        class="mx-2"
                ></v-select>
            </v-col>
            <v-col
                    class="v-col-3">
                <v-select
                        :items="fontSizes"
                        :model-value="fontSize"
                        @update:modelValue="setFontSize"
                        density="compact"
                        variant="underlined"
                ></v-select>
            </v-col>
        </v-row>
        <v-row>
            <v-col>
                <v-btn-toggle
                        :model-value="formatting"
                        @update:model-value="setFormatting"
                        multiple
                        variant="outlined"
                        divided
                >
                    <v-btn
                            :value="formattings.italic"
                            size="small"
                            icon="mdi-format-italic"
                    ></v-btn>
                    <v-btn
                            :value="formattings.bold"
                            size="small"
                            icon="mdi-format-bold"
                    ></v-btn>
                    <v-btn
                            :value="formattings.underlined"
                            size="small"
                            icon="mdi-format-underline"
                    ></v-btn>
                </v-btn-toggle>
            </v-col>
            <v-col>
                <ColorPicker
                        :model-value="fontColor"
                        @update:model-value="setFontColor"
                ></ColorPicker>

                <ColorPicker
                        :model-value="backColor"
                        icon="mdi-format-color-highlight"
                        @update:model-value="setBackColor"
                ></ColorPicker>
            </v-col>
        </v-row>
        <v-row>
            <v-col>
                <v-btn-toggle
                        :model-value="hAlign"
                        @update:model-value="setHAlign"
                        divided
                        variant="outlined"
                        mandatory
                >
                    <v-btn
                            icon="mdi-format-align-left"
                            :value="hAligns.left"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-align-center"
                            :value="hAligns.center"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-align-right"
                            :value="hAligns.right"
                            size="small"
                    ></v-btn>
                </v-btn-toggle>
            </v-col>
            <v-col>
                <v-btn-toggle
                        :model-value="vAlign"
                        @update:model-value="setVAlign"
                        divided
                        variant="outlined"
                        mandatory
                >
                    <v-btn
                            icon="mdi-format-vertical-align-top"
                            :value="vAligns.top"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-vertical-align-center"
                            :value="vAligns.center"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-vertical-align-bottom"
                            :value="vAligns.bottom"
                            size="small"
                    ></v-btn>
                </v-btn-toggle>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup lang="ts">
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import ColorPicker from "@/components/Properties/Common/ColorPicker.vue";
import * as Constants from "@/constants";
import {computed} from "vue";
import {IBlockFont} from "@/interfaces/Blocks";

const dStore = deviceStore()
const mStore = mainStore()

const vAligns = computed(() => Constants.vAlign)
const hAligns = computed(() => Constants.hAlign)
const formattings = computed(() => Constants.formatting)

const fontNames = computed(() => mStore.fontNames)
const fontSizes = computed(() => mStore.fontSizes)

const block = computed(() => dStore.block as IBlockFont | null)

const fontId = computed(() => block.value?.fontId)
const fontSize = computed(() => block.value?.fontSize)
const fontColor = computed(() => block.value?.fontColor ?? '#000000')
const backColor = computed(() => block.value?.backColor ?? '#ffffff')
const formatting = computed(() => block.value?.formatting ?? [])
const hAlign = computed(() => block.value?.hAlign ?? Constants.hAlign.left)
const vAlign = computed(() => block.value?.vAlign ?? Constants.vAlign.center)

const setFont = (id: number) => {
    if (!block.value)
        return
    block.value.fontId = id
}
const setFontSize = (size: number) => {
    if (!block.value)
        return
    block.value.fontSize = size
}
const setFontColor = (value: string) => {
    if (!block.value)
        return
    block.value.fontColor = value
}
const setBackColor = (value: string) => {
    if (!block.value)
        return
    block.value.backColor = value
}
const setFormatting = (values: Constants.formatting[]) => {
    if (!block.value)
        return
    block.value.formatting = values
}
const setHAlign = (value: Constants.hAlign) => {
    if (!block.value)
        return
    block.value.hAlign = value
}
const setVAlign = (value: Constants.vAlign) => {
    if (!block.value)
        return
    block.value.vAlign = value
}
</script>

<style scoped>

</style>