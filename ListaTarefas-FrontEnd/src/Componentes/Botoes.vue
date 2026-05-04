<script setup>
import { watch } from "vue";

const props = defineProps({
	filtro: Object,
});

const emit = defineEmits(["atualizar:filtro", "abrir-criar"]);

function atualizarBusca(valor) {
	emit("atualizar:filtro", { ...props.filtro, textoBusca: valor });
}
function atualizarStatus(valor) {
	emit("atualizar:filtro", { ...props.filtro, status: valor });
}
</script>

<template>
	<div class="botoes">
		<button class="btn-criar" @click="$emit('abrir-criar')">+ Criar</button>
		<input
			:value="props.filtro.textoBusca"
			@change="atualizarBusca($event.target.value)"
			placeholder="Pesquisar..."
			class="input-busca"
		/>
		<select
			:value="props.filtro.status"
			@change="atualizarStatus($event.target.value)"
			class="select-status"
		>
			<option value="">Todas</option>
			<option value="0">Pendente</option>
			<option value="1">Em Andamento</option>
			<option value="2">Concluída</option>
		</select>
	</div>
</template>

<style scoped>
.botoes {
	display: flex;
	align-items: center;
	justify-content: center;
	gap: 12px;
	padding: 1.5rem;
	background-color: var(--gray-600);
	border-bottom: 1px solid var(--gray-500);
}

.btn-criar {
	font-size: 1rem;
	font-weight: 700;
	color: var(--gray-100);
	background-color: var(--blue-dark);
	border: none;
	border-radius: 6px;
	padding: 10px 20px;
	cursor: pointer;
	transition: filter 0.15s ease;
}

.btn-criar:hover {
	filter: brightness(1.2);
}

.input-busca {
	font-size: 1rem;
	color: var(--gray-100);
	background-color: var(--gray-500);
	border: 1px solid var(--gray-400);
	border-radius: 6px;
	padding: 10px 14px;
	width: 260px;
	outline: none;
	transition: border-color 0.15s ease;
}

.input-busca::placeholder {
	color: var(--gray-300);
}

.input-busca:focus {
	border-color: var(--purple);
}

.select-status {
	font-size: 1rem;
	color: var(--gray-100);
	background-color: var(--gray-500);
	border: 1px solid var(--gray-400);
	border-radius: 6px;
	padding: 10px 14px;
	cursor: pointer;
	outline: none;
	transition: border-color 0.15s ease;
}

.select-status:focus {
	border-color: var(--purple);
}
</style>
