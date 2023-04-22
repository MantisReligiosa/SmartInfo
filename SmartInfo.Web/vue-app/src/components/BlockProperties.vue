<template>
    <v-container class="px-0">
        <v-text-field
                v-if="isBlockSelected"
                class="centered-input"
                density="compact"
                variant="underlined"
                v-model="block.caption"
                spellcheck="false"
                prepend-icon="mdi-rename-outline"
        ></v-text-field>
        <v-card
                v-if="isBlockSelected"
        >
            <GeometryProperties></GeometryProperties>
        </v-card>
        <v-card
                class="mt-1"
                v-if="blockHaveText"
        >
            <FontProperties></FontProperties>
        </v-card>
        <v-card>
            <component :is="getProperties"></component>
        </v-card>
    </v-container>
</template>

<script setup lang="ts">
import {deviceStore} from '@/store/deviceStore'
import * as Constants from "@/constants"
import {IBlock} from "@/interfaces/Blocks"
import GeometryProperties from "@/components/Properties/Common/GeometryProperties.vue"
import FontProperties from "@/components/Properties/Common/FontProperties.vue"
import TextBlockProperties from "@/components/Properties/TextBlockProperties.vue"
import DataBlockProperties from "@/components/Properties/DataBlockProperties.vue";
import PictureBlockProperties from "@/components/Properties/PictureBlockProperties.vue";
import BlankProperties from "@/components/Properties/BlankProperties.vue";
import {computed} from "vue";

const dStore = deviceStore()

const components = {
    TextBlockProperties,
    DataBlockProperties,
    PictureBlockProperties,
    BlankProperties
}

const block = computed(() => dStore.block as IBlock)
const isBlockSelected = computed(() => block.value && block.value.type)
const blockHaveText = computed(() => block.value
    && [Constants.blockType.text, Constants.blockType.table, Constants.blockType.dateTime].includes(block.value.type))

const getProperties = computed(() =>{
    if (!block.value)
        return components['BlankProperties']
    
    if (block.value.type === Constants.blockType.text)
        return components['TextBlockProperties']
    
    if (block.value.type === Constants.blockType.dateTime)
        return components['DataBlockProperties']
    
    if (block.value.type === Constants.blockType.picture)
        return components['PictureBlockProperties']
    
    return components['BlankProperties']
})
</script>

<style scoped>
:deep(.v-input__prepend) {
    margin-right: 0 !important;
}

:deep(.v-col) {
    padding: 0 !important;
}

.centered-input :deep(input) {
    text-align: center
}
</style>