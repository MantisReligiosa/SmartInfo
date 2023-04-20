<template>
    <v-container>
        <v-row>
            <v-text-field
                    variant="underlined"
                    v-model="block.text"
                    prepend-icon="mdi-text"
            ></v-text-field>
        </v-row>
        <v-row>
            <v-checkbox
                    label="Анимация"
                    v-model="block.isAnimationEnabled"
            ></v-checkbox>
        </v-row>
        <v-row>
            <v-select
                    v-model="block.animationStyle"
                    :items="animationStyles"
                    :disabled="!block.isAnimationEnabled"
            ></v-select>
        </v-row>
        <v-row>
            <v-col class="v-col-3">
                <v-text-field
                        hide-details
                        :disabled="!block.isAnimationEnabled"
                        single-line
                        type="number"
                        v-model="block.holdBeforeAnimation"
                        min="0"
                        max="99"
                        density="compact"
                        variant="underlined"
                        prepend-icon="mdi-clock-start"
                ></v-text-field>
            </v-col>
            <v-col class="v-col-4">
                <v-text-field
                        class="text-right ml-1"
                        hide-details
                        :disabled="!block.isAnimationEnabled"
                        single-line
                        type="number"
                        v-model="block.holdAfterAnimation"
                        min="0"
                        max="99"
                        density="compact"
                        variant="underlined"
                        append-icon="mdi-clock-end"
                ></v-text-field>
            </v-col>
            <v-col
                    class="justify-end"
            >
                <v-text-field
                        class="ml-4"
                        hide-details
                        :disabled="!block.isAnimationEnabled"
                        single-line
                        type="number"
                        v-model="block.animationSpeed"
                        min="1"
                        max="10"
                        density="compact"
                        variant="underlined"
                        prepend-icon="mdi-speedometer"
                ></v-text-field>
            </v-col>
        </v-row>
    </v-container>
</template>

<script setup lang="ts">
import {deviceStore} from "@/store/deviceStore";
import {computed, ref} from "vue";
import * as Constants from "@/constants"
import {ITextBlock} from "@/interfaces/Blocks";

const dStore = deviceStore()
const block = computed(() => dStore.block as ITextBlock)

const animationStyles = ref([
    {title: "Стиль 1", value: Constants.textAnimation.Style1},
    {title: "Стиль 2", value: Constants.textAnimation.Style2},
    {title: "Стиль 3", value: Constants.textAnimation.Style3}
])

</script>

<style scoped>
.text-right >>> input {
    text-align: right;
}
</style>