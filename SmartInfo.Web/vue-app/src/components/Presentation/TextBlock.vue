<script setup lang="ts">
import {computed, defineProps} from "vue";
import {ITextBlock} from "@/interfaces/Blocks";

const props = defineProps(['block'])
const block = computed(() => props.block as ITextBlock)
const color = computed(() => block.value.fontColor)
const backColor = computed(() => block.value.backColor)
</script>

<template>
<label class="textBlock">{{block?.text}}</label>
</template>

<style scoped>
.textBlock {
  color: v-bind(color);
  background-color: v-bind(backColor);
}
</style>