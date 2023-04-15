<template>
    <v-menu>
        <template
                v-slot:activator="{ props }">
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
                    <div class="colorIndicator" :style="{backgroundColor:value}"></div>
                    </span>
            </v-btn>
        </template>
        <v-color-picker v-model="value" flat></v-color-picker>
    </v-menu>
</template>

<script>
export default {
    props: ['modelValue', 'icon'],
    emits: ['update:modelValue'],
    computed: {
        value: {
            get() {
                return this.modelValue
            },
            set(value) {
                this.$emit('update:modelValue', value)
            }
        },
        pickerIcon() {
            return this.icon || 'mdi-format-color-text';
        }

    }
}
</script>

<style scoped>
div.colorIndicator {
    height: 3px;
    width: 100%;
}
</style>