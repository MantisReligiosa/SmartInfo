<template>
    <v-menu>
        <template
                v-slot:activator="{ props }"
        >
            <v-btn
                    v-bind="props"
                    class="px-1 h-100"
                    variant="outlined"
                    color="grey-lighten-2"
                    size="small"
            >
                <span
                        class="d-flex flex-column"
                >
                    <v-icon
                            :icon="pickerIcon"
                            color="black"
                    ></v-icon>
                    <div 
                            class="colorIndicator" 
                            :style="{backgroundColor:value}"
                    ></div>
                    </span>
            </v-btn>
        </template>
        <v-color-picker 
            v-model="value" 
            flat
        ></v-color-picker>
    </v-menu>
</template>

<script setup>
import {defineProps, defineEmits, computed} from 'vue'

const props = defineProps(['modelValue', 'icon'])
const emit = defineEmits(['update:modelValue'])

const pickerIcon = computed(() => {
    return props.icon || 'mdi-format-color-text'
})
const value = computed({
    get() {
        return props.modelValue
    },
    set(value) {
        emit('update:modelValue', value)
    }
})
</script>

<style scoped>
div.colorIndicator {
    height: 3px;
    width: 100%;
}
</style>