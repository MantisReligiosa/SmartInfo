<script setup lang="ts">
import {computed, defineProps} from "vue";
import PresentationBlock from "@/components/Presentation/PresentationBlock.vue";
import {ITableBlock} from "@/interfaces/Blocks";
import _ from "lodash"
import {hAlign} from "@/constants";

const props = defineProps(['block', 'scale'])
const block = computed(() => props.block as ITableBlock)
const headerCells = computed(() => block.value.cells.filter(c => c.row === 0))
const rows = computed(() =>
    _.groupBy(
        block.value.cells
            .filter(c => c.row > 0), c => c.row
    ))

const headerBgColor = computed(() => block.value?.headerDetails.backColor)
const headerTextColor = computed(() => block.value?.headerDetails.fontColor)
const headerHAlign = computed(() => block.value?.headerDetails.hAlign === hAlign.left ? "left"
    : block.value?.headerDetails.hAlign === hAlign.right ? "right" : "justify")
const headerFontSize = computed(() => block.value?.headerDetails.fontSize * props.scale + 'px')

const evenBgColor = computed(() => block.value?.evenRowsDetails.backColor)
const evenTextColor = computed(() => block.value?.evenRowsDetails.fontColor)
const evenHAlign = computed(() => block.value?.evenRowsDetails.hAlign === hAlign.left ? "left"
    : block.value?.evenRowsDetails.hAlign === hAlign.right ? "right" : "justify")
const evenFontSize = computed(() => block.value?.evenRowsDetails.fontSize * props.scale + 'px')


const oddBgColor = computed(() => block.value?.oddRowsDetails.backColor)
const oddTextColor = computed(() => block.value?.oddRowsDetails.fontColor)
const oddHAlign = computed(() => block.value?.oddRowsDetails.hAlign === hAlign.left ? "left"
    : block.value?.oddRowsDetails.hAlign === hAlign.right ? "right" : "justify")
const oddFontSize = computed(() => block.value?.oddRowsDetails.fontSize * props.scale + 'px')

const getColumnWidth = function (index: number) {
  return (block.value?.columnWidths.find(w => w.index === index)?.value ?? 0) * props.scale + 'px'
}

</script>

<template>
  <presentation-block
      v-bind:block="props.block"
      v-bind:scale="props.scale"
  >
    <table
        class="tableStyle"
    >
      <thead
          class="headerStyle"
      >
      <tr>
        <td
            v-for="(h, index) in headerCells"
            v-bind:key="index"
            :style="{width:getColumnWidth(index)}"
        >
          {{ h.value }}
        </td>
      </tr>
      </thead>
      <tbody
          v-for="(row, index) in rows"
          v-bind:key="index"
          :class="(index%2==0) ? 'evenRowStyle':'oddRowStyle'"
      >
      <tr>
        <td
            v-for="(c, index) in row"
            v-bind:key="index"
        >
          {{ c.value }}
        </td>
      </tr>
      </tbody>
    </table>
  </presentation-block>
</template>

<style scoped>
.headerStyle {
  color: v-bind(headerTextColor);
  font-size: v-bind(headerFontSize);
  text-align: v-bind(headerHAlign);
  background-color: v-bind(headerBgColor);
  border: 0;
}

.tableStyle {
  border-spacing: 0;
}

.evenRowStyle {
  color: v-bind(evenTextColor);
  font-size: v-bind(evenFontSize);
  text-align: v-bind(evenHAlign);
  background-color: v-bind(evenBgColor);
}

.oddRowStyle {
  color: v-bind(oddTextColor);
  font-size: v-bind(oddFontSize);
  text-align: v-bind(oddHAlign);
  background-color: v-bind(oddBgColor)
}
</style>