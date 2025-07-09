CREATE EXTENSION IF NOT EXISTS pgcrypto;

DO $$
DECLARE
    i INTEGER := 0;
    fnames TEXT[] := ARRAY['John', 'Jane', 'Alice', 'Bob', 'Charlie', 'Emily', 'Michael', 'Sarah', 'David', 'Laura'];
    lnames TEXT[] := ARRAY['Smith', 'Johnson', 'Williams', 'Brown', 'Jones', 'Garcia', 'Miller', 'Davis', 'Rodriguez', 'Martinez'];
	dcities TEXT[] := ARRAY['Kaunas', 'Vilnius', 'Klaipėda', 'Marijampolė', 'Prienai', 'Birštonas', 'Utena', 'Panevėžys'];
    f TEXT;
    l TEXT;
    e TEXT;
    d TEXT;
BEGIN
    WHILE i < 10000 LOOP
        f := fnames[1 + floor(random() * array_length(fnames, 1))::int];
        l := lnames[1 + floor(random() * array_length(lnames, 1))::int];
        
        e := lower(f || '.' || l || '.' || substr(md5(random()::text), 1, 6) || '@example.com');
        d := dcities[1+floor(random() * array_length(dcities, 1))::int];

		INSERT INTO customers (first_name, last_name, email, details)
		VALUES (f, l, e,  CONCAT('{"country":"Lithuania","city":"',d,'"}')::jsonb);

        i := i + 1;
    END LOOP;
END $$;

DO $$
DECLARE
    i INTEGER := 0;
    names TEXT[] := ARRAY['Išmanusis telefonas','Nešiojamas kompiuteris','Sportiniai batai','Kavos aparatas','Vaikiškas dviratis','LED televizorius','Kepimo forma','Dviračio šalmas','Elektrinis skustuvas','Grožio serumas','Marškinėliai vyrams','Knyga „Kelionės“','Virtuvinis kombainas','Sodo laistytuvas','Automobilio padanga','Ledinė kavos mašina','Biuro kėdė','Rankinė moterims','Ausinės bevielės','Plaukų džiovintuvas'];
	categories TEXT[] := ARRAY['Elektronika','Drabužiai','Namų apyvokos reikmenys','Sporto prekės','Grožio ir kosmetikos produktai','Vaikų žaislai','Maisto produktai','Knygos ir leidiniai','Biuro prekės','Sodo įranga ir reikmenys','Automobilių dalys ir aksesuarai','Buitinė technika','Mados aksesuarai','Sveikatos ir higienos prekės','Statybinės medžiagos'];
    n TEXT;
    c TEXT;
	p decimal(10,2);
BEGIN
    WHILE i < 8000 LOOP
        n := names[1 + floor(random() * array_length(names, 1))::int];
        c := categories[1 + floor(random() * array_length(categories, 1))::int];
		p := random() * (random() * 100);
		
		INSERT INTO products ("name", category, price)
		VALUES (n, c, p);

        i := i + 1;
    END LOOP;
END $$;

DO $$
DECLARE
    i INTEGER := 0;
    cust_id INT;
    prod_id INT;
    prod_count INT;
    prod_ids INT[];
    details_count INT;
    ord_id INT;
    qty INT;
    price DECIMAL(10,2);
	total_customers INT;
BEGIN
    SELECT array_agg(product_id) INTO prod_ids FROM products;
    prod_count := array_length(prod_ids, 1);
	total_customers := (SELECT COUNT(*) FROM customers);

    WHILE i < 100000 LOOP
	
        SELECT customer_id INTO cust_id FROM customers 
		OFFSET floor(random() * total_customers) LIMIT 1;

        INSERT INTO orders(customer_id)
        VALUES (cust_id)
        RETURNING order_id INTO ord_id;

        details_count := 1 + floor(random() * 100)::int;

        FOR j IN 1..details_count LOOP
            prod_id := prod_ids[1 + floor(random() * prod_count)::int];

            qty := 1 + floor(random() * 50)::int;

            INSERT INTO order_details(order_id, product_id, quantity)
            VALUES (ord_id, prod_id, qty);
        END LOOP;

        i := i + 1;
    END LOOP;
END $$;
