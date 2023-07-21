<script setup lang="ts">
import {computed, defineProps} from "vue";
import {ITextBlock} from "@/interfaces/Blocks";

const props = defineProps(['block'])
const block = computed(() => props.block as ITextBlock)
</script>

<template>
<div>{{block?.text}}</div>
</template>

<style scoped>

</style>