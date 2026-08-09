# Discounting

A small service that calculates customer discounts for order subtotals based on customer tier and recent order history.

## Language

**Customer**:
A person or organization that places orders and may qualify for discounts.
_Avoid_: client, buyer, account

**Customer tier**:
A profile attribute that categorizes customers into levels such as Bronze, Silver, or Gold.
_Avoid_: loyalty status, membership level

**Past orders**:
Completed orders placed by a customer within the last 12 months, excluding cancelled or refunded orders.
_Avoid_: order history, history

**Order subtotal**:
The price of an order before tax, shipping, or discounts.
_Avoid_: total, cart value

**Discountable amount**:
The portion of an order subtotal that is eligible for a discount.
_Avoid_: discount base, discount total

**Discount**:
A reduction applied to a discountable amount, expressed as a percentage and a monetary amount.
_Avoid_: coupon, rebate

**Tier base discount**:
The baseline discount percentage granted by a customer's tier.
_Avoid_: tier discount

**History modifier**:
An additional discount adjustment derived from the customer's past orders.
_Avoid_: loyalty bonus

**Discount reason**:
A human-readable explanation for why a discount was applied or why none was given.
_Avoid_: justification
