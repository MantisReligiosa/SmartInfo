<template>
    <v-container>
        <v-row>
            <v-text-field
                    class="centered-input"
                    density="compact"
                    variant="underlined"
                    v-model="device.name"
                    spellcheck="false"
                    prepend-icon="mdi-rename-outline"
            ></v-text-field>
        </v-row>
        <v-row>
            <color-picker
                    icon="mdi-palette"
                    :model-value="backColor"
                    @update:model-value="setBackColor"
            ></color-picker>
        </v-row>
        <v-row>
            {{ resolution.width }}x{{ resolution.height }}px
        </v-row>
    </v-container>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";
import {IDevice} from "@/interfaces/IDevice";
import ColorPicker from "@/components/Properties/Common/ColorPicker.vue";

const dStore = deviceStore()
const device = computed(() => dStore.device as IDevice)
const resolution = computed(() => dStore.resolution)
const backColor = computed(() => device.value?.backColor ?? '#ffffff')

const setBackColor = (value: string) => {
    if (!device.value)
        return
    device.value.backColor = value
}
</script>

<style scoped>
.centered-input :deep(input) {
    text-align: center
}
</style>