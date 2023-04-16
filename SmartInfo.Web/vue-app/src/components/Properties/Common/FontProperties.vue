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
                        v-model="block.formatting"
                        multiple
                        variant="outlined"
                        divided
                >
                    <v-btn
                            :value="formatting.italic"
                            size="small"
                            icon="mdi-format-italic"
                    ></v-btn>
                    <v-btn
                            :value="formatting.bold"
                            size="small"
                            icon="mdi-format-bold"
                    ></v-btn>
                    <v-btn
                            :value="formatting.underlined"
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
                        v-model="block.hAlign"
                        divided
                        variant="outlined"
                        mandatory
                >
                    <v-btn
                            icon="mdi-format-align-left"
                            :value="hAlign.left"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-align-center"
                            :value="hAlign.center"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-align-right"
                            :value="hAlign.right"
                            size="small"
                    ></v-btn>
                </v-btn-toggle>
            </v-col>
            <v-col>
                <v-btn-toggle
                        v-model="block.vAlign"
                        divided
                        variant="outlined"
                        mandatory
                >
                    <v-btn
                            icon="mdi-format-vertical-align-top"
                            :value="vAlign.top"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-vertical-align-center"
                            :value="vAlign.center"
                            size="small"
                    ></v-btn>
                    <v-btn
                            icon="mdi-format-vertical-align-bottom"
                            :value="vAlign.bottom"
                            size="small"
                    ></v-btn>
                </v-btn-toggle>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup>
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'
import ColorPicker from "@/components/Properties/Common/ColorPicker.vue";
import * as Constants from "@/constants";
import {computed} from "vue";

const store = deviceStore()
const mStore = mainStore()

const vAlign = computed(() => Constants.vAlign)
const hAlign = computed(() => Constants.hAlign)
const formatting = computed(() => Constants.formatting)

const block = computed(() => store.block)

const fontNames = computed(() => mStore.fontNames)
const fontSizes = computed(() => mStore.fontSizes)

const fontId = computed(() => block.value.fontId)
const fontSize = computed(() => block.value.fontSize)
const fontColor = computed(() => block.value.fontColor)
const backColor = computed(() => block.value.backColor)

const setFont = (id) => block.value.fontId = id
const setFontSize = (size) => block.value.fontSize = size
const setFontColor = (value) => block.value.fontColor = value
const setBackColor = (value) => block.value.backColor = value
</script>

<style scoped>

</style>