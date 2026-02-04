module game_package::pet_module {
    use sui::object::{Self, UID};
    use sui::transfer;
    use sui::tx_context::{Self, TxContext};
    use sui::coin::{Self, Coin};
    use sui::balance::Balance;
    use sui::sui::SUI;
    use std::string::String;

    struct Pet has key, store {
        id: UID,
        name: String,
        health: u64,
        strength: u64,
        defense: u64,
        agility: u64,
        bank: Balance<SUI>
    }
    public fun create_pet(
        payment: &mut Coin<SUI>, 
        amount_for_pet: u64, 
        name: String, 
        health: u64,
        strength: u64, 
        defense: u64,
        agility: u64, 
        ctx: &mut TxContext
    ) {
        let coin_for_pet = coin::split(payment, amount_for_pet, ctx);
        
        let balance_for_pet = coin::into_balance(coin_for_pet);

        let new_pet = Pet {
            id: object::new(ctx),
            name: name,
            health: health,
            strength: strength,
            defense: defense,
            agility: agility,
            bank: balance_for_pet
        };

        transfer::public_transfer(new_pet, tx_context::sender(ctx));
    }
}