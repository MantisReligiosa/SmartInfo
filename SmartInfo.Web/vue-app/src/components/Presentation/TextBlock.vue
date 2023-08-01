<script setup lang="ts">
import {computed, defineProps} from "vue";
import {ITextBlock} from "@/interfaces/Blocks";
import PresentationBlock from "@/components/Presentation/PresentationBlock.vue";

const props = defineProps(['block', 'scale'])
const block = computed(() => props.block as ITextBlock)
const isAnimationEnabled = computed(() => block.value.isAnimationEnabled)
const animationName = computed(() => {
  switch (block.value.animationStyle) {
    case 0:
      return 'ltrAnimation';
    case 1:
      return 'rtlAnimation';
    default:
      return 'utdAnimation';

  }
})
const animationDuration = computed(() => (11 - block.value.animationSpeed) + 's')
</script>

<template>
  <presentation-block
      class="block"
      v-bind:block="props.block"
      v-bind:scale="props.scale"
  >
    <div
        class="vAlignStyle h-100"
        :class="{marquee:isAnimationEnabled}"
    >{{ block?.text }}
    </div>
  </presentation-block>
</template>

<style scoped>
.block {
  overflow: hidden;
}

.marquee {
  animation-name: v-bind(animationName);
  animation-iteration-count: infinite;
  animation-timing-function: ease;
  animation-play-state: running;
  animation-duration: v-bind(animationDuration);
}


</style>

<style>
@keyframes ltrAnimation {
  0% {
    transform: translateX(-100%);
  }
  100% {
    transform: translateX(100%);
  }
}

@keyframes rtlAnimation {
  0% {
    transform: translateX(100%);
  }
  100% {
    transform: translateX(0);
  }
}

@keyframes utdAnimation {
  0% {
    transform: translateY(0);
  }
  100% {
    transform: translateY(100%);
  }
}
</style>