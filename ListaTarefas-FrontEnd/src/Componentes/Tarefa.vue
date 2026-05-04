<script setup>
defineProps({
	titulo: String,
	descricao: String,
	status: String,
	dataDeCriacao: String,
});

const statusLabel = {
	0: { texto: "Pendente", classe: "status-pendente" },
	1: { texto: "Em Andamento", classe: "status-andamento" },
	2: { texto: "Concluída", classe: "status-concluida" },
};

function formatarData(iso) {
	if (!iso) return "";
	return new Date(iso).toLocaleDateString("pt-BR");
}

defineEmits(["editar", "deletar"]);
</script>

<template>
	<div class="Card">
		<div class="TituloEStatus">
			<h1 class="Titulo">{{ titulo }}</h1>
			<span class="StatusBadge" :class="statusLabel[status]?.classe">
				{{ statusLabel[status]?.texto }}
			</span>
		</div>
		<div>
			<p class="Descricao">{{ descricao }}</p>
		</div>
		<span class="DataCriada">{{ formatarData(dataDeCriacao) }}</span>
		<div class="Acoes">
			<button class="Editar" @click="$emit('editar')">Editar</button>
			<button class="Deletar" @click="$emit('deletar')">Deletar</button>
		</div>
	</div>
</template>

<style scoped>
.TituloEStatus {
	display: flex;
	align-items: center;
	gap: 10px;
}

.Card {
	width: 100%;
	max-width: 800px;
	position: relative;
	padding: 20px 24px;
	border: 1px solid var(--gray-400);
	border-radius: 8px;
	background-color: var(--gray-500);
	transition: border-color 0.2s ease;
}

.Card:hover {
	border-color: var(--purple);
}

.Titulo {
	font-size: 1.5rem;
	font-weight: 700;
	color: var(--gray-100);
	width: fit-content;
}

.Descricao {
	font-size: 1rem;
	font-weight: 400;
	color: var(--gray-300);
	margin: 0 0 10px 0;
}

.DataCriada {
	font-size: 0.75rem;
	color: var(--gray-300);
}

.Acoes {
	position: absolute;
	top: 20px;
	right: 20px;
	display: flex;
	gap: 8px;
}

.Editar,
.Deletar {
	font-size: 0.8rem;
	font-weight: 600;
	padding: 6px 14px;
	border-radius: 6px;
	border: none;
	cursor: pointer;
	transition: filter 0.15s ease;
}

.Editar:hover,
.Deletar:hover {
	filter: brightness(1.15);
}

.Editar {
	background-color: var(--purple-dark);
	color: var(--gray-100);
}

.Deletar {
	background-color: transparent;
	color: var(--danger);
	border: 1px solid var(--danger);
}

.status-pendente {
	color: #f59e0b;
	border: 1px solid #f59e0b;
	padding: 2px 10px;
	border-radius: 999px;
	font-size: 0.7rem;
	font-weight: 600;
}

.status-andamento {
	color: #3b82f6;
	border: 1px solid #3b82f6;
	padding: 2px 10px;
	border-radius: 999px;
	font-size: 0.7rem;
	font-weight: 600;
}

.status-concluida {
	color: #22c55e;
	border: 1px solid #22c55e;
	padding: 2px 10px;
	border-radius: 999px;
	font-size: 0.7rem;
	font-weight: 600;
}
</style>
