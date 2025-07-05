-- Performance
/*
Tested on PostgreSQL server running in WSL Docker.

Invoice query execution time: ~39 ms  
Orders distribution by city: ~600 ms
*/

-- Invoice
/*
This query returns the details of a single invoice, including:
- Product category
- Product name
- Quantity of product
- Unit price (amount)
- Total paid amount for the product (total amount)
*/
SELECT
	P.CATEGORY AS PRODUCT_CATEGORY,
	P.NAME AS PRODUCT_NAME,
	OD.QUANTITY,
	P.PRICE AS AMOUNT,
	(OD.QUANTITY * P.PRICE) AS TOTAL_AMOUNT
FROM
	ORDER_DETAILS OD
	INNER JOIN PRODUCTS P ON OD.PRODUCT_ID = P.PRODUCT_ID
WHERE
	OD.ORDER_ID = 10 -- only one invoice's details
	-- 	P.NAME LIKE '%Sodo%'
	-- AND P.CATEGORY LIKE '%Drabužiai%'
ORDER BY
	-- P.NAME, 
	OD.ORDER_ID;

-------------------------------------------------------
-------------------------------------------------------
-------------------------------------------------------
-- Orders Distribution by City
/*
INNER JOIN (...) AS OD – The subquery executes once and contains all the details needed for the report.
A materialized view could be used instead, but it would require a refresh after new orders are added.
*/
SELECT
	C.CITY AS CUSTOMER_CITY,
	COUNT(O.ORDER_ID) AS NUMBER_OF_ORDERS,
	SUM(OD.ORDER_AMOUNT) AS TOTAL_AMOUNT
FROM
	ORDERS O
	INNER JOIN CUSTOMERS C ON O.CUSTOMER_ID = C.CUSTOMER_ID
	INNER JOIN (
		SELECT
			OD.ORDER_ID,
			SUM(OD.QUANTITY * P.PRICE) ORDER_AMOUNT
		FROM
			ORDER_DETAILS OD
			INNER JOIN PRODUCTS P ON OD.PRODUCT_ID = P.PRODUCT_ID
		GROUP BY
			OD.ORDER_ID
	) AS OD ON O.ORDER_ID = OD.ORDER_ID
	-- WHERE
	-- 	C.CITY = 'Kaunas'
GROUP BY
	C.CITY
ORDER BY
	COUNT(O.ORDER_ID);