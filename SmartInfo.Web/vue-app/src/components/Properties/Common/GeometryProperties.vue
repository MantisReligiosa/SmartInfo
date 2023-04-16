<template>
    <v-container>
        <v-row>
            <v-col>
                <v-text-field
                        hide-details
                        single-line
                        type="number"
                        v-model="block.x"
                        min="0"
                        density="compact"
                        variant="underlined"
                        prepend-icon="mdi-alpha-x"
                ></v-text-field>
            </v-col>
            <v-col>
                <v-text-field
                        hide-details
                        single-line
                        type="number"
                        v-model="block.y"
                        min="0"
                        density="compact"
                        variant="underlined"
                        prepend-icon="mdi-alpha-y"
                ></v-text-field>
            </v-col>
        </v-row>
        <v-row>
            <v-col>
                <v-text-field
                        hide-details
                        single-line
                        type="number"
                        v-model="block.h"
                        min="0"
                        density="compact"
                        variant="underlined"
                        prepend-icon="mdi-alpha-h"
                ></v-text-field>
            </v-col>
            <v-col>
                <v-text-field
                        hide-details
                        single-line
                        type="number"
                        v-model="block.w"
                        min="0"
                        density="compact"
                        variant="underlined"
                        prepend-icon="mdi-alpha-w"
                ></v-text-field>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup>
import {deviceStore} from "@/store/deviceStore";
import {computed} from "vue";

const store = deviceStore()
const block = computed(() => store.block)

</script>

<style scoped>

</style>