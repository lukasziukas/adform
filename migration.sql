/*
Foreign keys for data integrity.
Indexes for faster joins between tables.
City stored calculated stored field for index and faster search.
*/

create table if not exists customers(
	customer_id serial not null primary key,
	first_name varchar(256) not null,
	last_name varchar(256) not null,
	email varchar(320) not null,
	details jsonb not null
);

alter table customers
add if not exists city text generated always as (details::json->>'city') stored;

create index if not exists ix_customers_city on customers(city);

create table if not exists products(
	product_id serial not null primary key,
	name varchar(450) not null,
	category varchar(450) not null,
	price decimal(10, 2) not null
);

create table if not exists orders(
	order_id serial not null primary key,
	customer_id int not null,
	constraint fk_orders_customers_customer_id foreign key (customer_id) references customers(customer_id)
);

create index if not exists ix_orders_customer_id on orders(customer_id);

create table if not exists order_details(
	order_details_id serial not null primary key,
	order_id int not null,
	product_id int not null,
	quantity int not null,
	constraint fk_order_details_orders_order_id foreign key (order_id) references orders(order_id),
	constraint fk_order_details_products_product_id foreign key (product_id) references products(product_id)
);

create index if not exists ix_order_details_order_id on order_details(order_id);
create index if not exists ix_order_details_product_id on order_details(product_id);