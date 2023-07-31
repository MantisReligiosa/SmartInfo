<script setup lang="ts">
import {defineProps} from "vue";
import PresentationBlock from "@/components/Presentation/PresentationBlock.vue";

const props = defineProps(['block', 'scale'])
</script>

<template>
  <presentation-block
      v-bind:block="props.block"
      v-bind:scale="props.scale"
  >+-TABLE-+</presentation-block>
</template>

<style scoped>
</style>